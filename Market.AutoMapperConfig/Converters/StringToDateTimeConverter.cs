using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.AutoMapperConfig.Converters
{
    public class StringToDateTimeConverter : AutoMapper.ITypeConverter <string,DateTime?>
    {
        #region Implementation of ITypeConverter<in string,DateTime>

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param><param name="destination">Destination object</param><param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public DateTime? Convert ( string source ,DateTime? destination ,AutoMapper.ResolutionContext context )
        {
            if ( source == null )
                return null;
            return null; // source.ToMiladiDate();
        }

       
        #endregion
    }
}
