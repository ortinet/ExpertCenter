using ExpertCenter.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Repository.EntityFramework
{
    internal static class EFModelConverter
    {
        public static ColumnType Convert(this EFColumnType efColumnType)
        {
            return new ColumnType()
            {
                Id = efColumnType.Id,
                Code = efColumnType.Code,
                Title = efColumnType.Title,
            };
        }

        public static UserColumn Convert(this EFUserColumn efUserColumn)
        {
            return new UserColumn()
            {
                Id = efUserColumn.Id,
                Header = efUserColumn.Header,
                //Type = efUserColumn.
            };
        }
    }
}
