using rules.framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Rules
{
    public class BonusAmountRule: ExternalRule<Sale>, IBonusAmountRule
    {
    }
}
