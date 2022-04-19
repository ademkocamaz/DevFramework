using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Utilities.Mappings.AutoMapper
{
    public class AutoMapperHelper
    {
        public static List<T> MapToSameTypeList<T>(List<T> list)
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<T, T>();
            });

            List<T> result = Mapper.Map<List<T>, List<T>>(list);
            return result;
        }

        public static T MapToSameType<T>(T entity)
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<T, T>();
            });

            T result = Mapper.Map<T,T>(entity);
            return result;
        }

    }
}
