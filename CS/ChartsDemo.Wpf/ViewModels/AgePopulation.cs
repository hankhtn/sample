using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ChartsDemo {
    public class AgePopulation {
        string name;
        string age;
        string sex;
        double population;

        public string Name { get { return name; } }
        public string Age { get { return age; } }
        public string Sex { get { return sex; } }
        public string SexAgeKey { get { return sex.ToString() + ": " + age; } }
        public string CountryAgeKey { get { return name + ": " + age; } }
        public string CountrySexKey { get { return name + ": " + sex.ToString(); ; } }
        public double Population { get { return population; } }

        public AgePopulation(string name, string age, string gender, double population) {
            this.name = name;
            this.age = age;
            this.sex = gender;
            this.population = population;
        }
    }

}
