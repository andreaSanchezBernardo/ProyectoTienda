//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class CARRITO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARRITO()
        {
            this.DETALLE_VENTA = new HashSet<DETALLE_VENTA>();
        }
    
        public int IdCarrito { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdProducto { get; set; }
        public Nullable<int> Cantidad { get; set; }
    
        public virtual USUARIO USUARIO { get; set; }
        public virtual PRODUCTO PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_VENTA> DETALLE_VENTA { get; set; }
    }
}
