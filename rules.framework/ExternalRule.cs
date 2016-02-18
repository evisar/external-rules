using CSScriptLibrary;
using System.ComponentModel.Composition.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rules.framework
{
    public abstract class ExternalRule<T> : IExternalRule<T>
    {
        readonly MethodDelegate _action;
        //must use cache, otherwise will re-compile every time
        readonly static Dictionary<string, MethodDelegate> _delegateCache
            = new Dictionary<string, MethodDelegate>();

        public ExternalRule()
        {
            var name = this.GetType().Name + ".rule";
            var sb = new StringBuilder();
            sb.AppendLine(@"
            using System;
            using System.Collections.Generic;
            using System.IO;
            using System.Linq;
            using System.Text;");
            sb.AppendLine(string.Format("using {0};", typeof(T).Namespace));
            var rule = File.ReadAllText(Path.Combine("Rules\\", name));
            sb.AppendLine("void Rule(" + typeof(T).FullName + " " + typeof(T).Name.ToLower() +"){"+ rule+"}");
            
            if (!_delegateCache.ContainsKey(rule))
            {
                _delegateCache.Add(rule, CSScript.Evaluator.CreateDelegate(sb.ToString()));
            }
            _action = _delegateCache[rule];
        }

        public void ApplyFor(T item)
        {
            _action(item);
        }
    }
}
