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
    
    public partial class t_Categoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_Categoria()
        {
            this.t_Producto = new HashSet<t_Producto>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public string Categoria { get; set; }
        public Nullable<int> IdUsuarioCreo { get; set; }
        public Nullable<System.DateTime> FechaCreo { get; set; }
        public Nullable<int> IdUsuarioBorro { get; set; }
        public Nullable<bool> Borrado { get; set; }
        public Nullable<System.DateTime> FechaBorrado { get; set; }
        public string Imagen { get; set; }
        public Nullable<int> IdTienda { get; set; }
    
        public virtual t_Cliente t_Cliente { get; set; }
        public virtual t_Usuario t_Usuario { get; set; }
        public virtual t_Usuario t_Usuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_Producto> t_Producto { get; set; }
        public virtual t_Tienda t_Tienda { get; set; }
    }
}
