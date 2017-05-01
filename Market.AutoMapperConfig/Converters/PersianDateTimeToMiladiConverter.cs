using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarket.Utilities;

namespace Market.AutoMapperConfig.Converters
{
   public class PersianDateTimeToMiladiConverter : AutoMapper.ITypeConverter<DateTime?, DateTime?>
    {
        #region Implementation of ITypeConverter<in DateTime?,DateTime?>

        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="source">Source object</param><param name="destination">Destination object</param><param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public DateTime? Convert ( DateTime? source ,DateTime? destination ,AutoMapper.ResolutionContext context )
        {
            if (source == null)
                return null;
            
            return  source.Value.ToMiladiDate();
        }

        #endregion
    }
}
