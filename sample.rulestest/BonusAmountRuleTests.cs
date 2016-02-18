using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using domain;
using domain.Rules;
using rules.framework;
using Autofac;
using ImpromptuInterface;
using Autofac.Core;
using System.Linq;
using System.ComponentModel.Composition.Hosting;
using System.Collections.Generic;

namespace sample.rulestest
{
    [TestClass]
    public class BonusAmountRuleTests
    {
        #region Bootstrap Rules

        static readonly IContainer _container;

        static T New<T>()
        {
            return _container.Resolve<T>();
        }

        static BonusAmountRuleTests()
        {
            new DirectoryCatalog(".");

            var externalRules =
                from t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                where t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(ExternalRule<>)
                select t;

            var rules =
                from t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                where !t.IsAbstract && !t.IsInterface
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRule<>)
                select t;

            var builder = new ContainerBuilder();
            builder.RegisterTypes(externalRules.Union(rules).Distinct().ToArray()).AsClosedTypesOf(typeof(IRule<>)).AsImplementedInterfaces();
            builder.RegisterType<SalesLogic>();

            _container = builder.Build();
        }
        #endregion

        [TestMethod]
        public void BothInternalAndExternalBonusAmountRule_IsExecuted()
        {
            var sale = new Sale { Amount = 1000 };
            var logic = New<SalesLogic>();

            logic.Calculate(sale);

            Assert.IsTrue(sale.Bonus);
            Assert.IsTrue(sale.IsPromotion);
        }

    }
}
