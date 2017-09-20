using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public interface IPageBase
    {
        #region Members

        int? ItemId { get; set; } //Llave

        int PageCurrent { get; set; }

        int PageNumber { get; set; }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        int PageSize { get; set; }

        int PageTotalItems { get; set; }

        #endregion
    }
}
