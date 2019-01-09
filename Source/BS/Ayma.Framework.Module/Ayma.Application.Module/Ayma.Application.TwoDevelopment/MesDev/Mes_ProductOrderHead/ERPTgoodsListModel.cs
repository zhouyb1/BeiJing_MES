using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Application.TwoDevelopment.MesDev.Mes_ProductOrderHead
{
   public class ERPTgoodsListModel
    {

        /// <summary>
        /// 商品编码
        /// </summary>
        public string partno { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string pname { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string style { get; set; }

        /// <summary>
        /// 商品采购价格
        /// </summary>
        public string pur_price { get; set; }

        /// <summary>
        /// 商品售价
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 商品单位
        /// </summary>
        public string pack_uom { get; set; }

        /// <summary>
        /// 商品包装规格
        /// </summary>
        public string pack_qty { get; set; }

        /// <summary>
        /// 商品采购税率
        /// </summary>
        public string shuilv { get; set; }

        /// <summary>
        /// 商品销售税率
        /// </summary>
        public string shuilv_out { get; set; }

    }
}
