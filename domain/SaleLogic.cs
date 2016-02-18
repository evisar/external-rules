using domain.Rules;
using rules.framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class SalesLogic
    {
        readonly IEnumerable<IRule<Sale>> _rules;

        public SalesLogic(IEnumerable<IRule<Sale>> rules)
        {
            _rules = rules;
        }

        public void Calculate(Sale sale)
        {
            //do something imprtant with sales
            //
            //

            //apply bonus rules
            var bonusRules = _rules.OfType<IBonusAmountRule>();
            bonusRules.ToList().ForEach(x => x.ApplyFor(sale));

            //apply other logic
            //
            //

            //apply any other non bonus rules
            var otherRules = _rules.Where( x=> !bonusRules.Contains(x));
            otherRules.ToList().ForEach(x => x.ApplyFor(sale));
        }
    }
}
