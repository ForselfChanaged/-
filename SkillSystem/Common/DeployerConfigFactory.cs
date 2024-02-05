using System;
using System.Collections.Generic;


    class DeployerConfigFactory
    {
        public static ISelector CreateISelector(SkillData data)
        {
            string name = data.seletorType.ToString();
           return  CreateEffect<ISelector>(name);
        }
        public static List<IImpact> CreateIImpact(SkillData data,List<IImpact> list)
        {
            foreach (var impact in data.impactType)
            {
                string name =  impact.ToString();
                IImpact item = CreateEffect<IImpact>(name);
                list.Add(item);
            }
            return list;
        }
        private static  T CreateEffect<T>(string name) where T : class
        {
            Type type = Type.GetType(name);
            return Activator.CreateInstance(type) as T;
        }
    }

