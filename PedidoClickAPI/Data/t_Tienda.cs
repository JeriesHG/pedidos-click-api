//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PedidoClickAPI.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_Tienda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_Tienda()
        {
            this.t_Orden = new HashSet<t_Orden>();
            this.t_Producto = new HashSet<t_Producto>();
            this.t_Categoria = new HashSet<t_Categoria>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public Nullable<bool> Borrado { get; set; }
        public string Observaciones { get; set; }
        public string Paginaweb { get; set; }
        public Nullable<int> IdCategoriaTienda { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_Orden> t_Orden { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_Producto> t_Producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_Categoria> t_Categoria { get; set; }
        public virtual t_CategoriaTienda t_CategoriaTienda { get; set; }
    }
}
