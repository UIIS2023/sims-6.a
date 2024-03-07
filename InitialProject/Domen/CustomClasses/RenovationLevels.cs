using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public static class RenovationLevels
    {
        public static Dictionary<string, string> Levels { get; } = new Dictionary<string, string>
        {
            { "Level 1", "It would be nice to renovate some minor things, but everything works fine without it." },
            { "Level 2", "Minor complaints about the accommodation that, if addressed, would make it perfect." },
            { "Level 3", "Several things that bothered me should be renovated." },
            { "Level 4", "There are a lot of issues and renovation is really necessary." },
            { "Level 5", "The accommodation is in very poor condition and not worth renting unless renovated." }
        };
    }

}
