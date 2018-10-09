using DevExpress.Mvvm.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    [MetadataType(typeof(DataAnnotationsElement4Metadata))]
    public class DataAnnotationsElement4_FluentAPI {
        public int OrderId { get; set; }
        public CategoryData ProductCategory { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsReady { get; set; }
    }

    public static class DataAnnotationsElement4Metadata {
        public static void BuildMetadata(MetadataBuilder<DataAnnotationsElement4_FluentAPI> builder) {
            builder.TableLayout()
                .Group("Product Details")
                    .ContainsProperty(p => p.ProductCategory)
                    .ContainsProperty(p => p.ProductName)
                .EndGroup()
                .GroupContainer("Order Details")
                    .Group("Main")
                        .ContainsProperty(p => p.CustomerName)
                        .ContainsProperty(p => p.OrderDate)
                    .EndGroup()
                    .Group("Status")
                        .ContainsProperty(p => p.Quantity)
                        .ContainsProperty(p => p.Price)
                        .ContainsProperty(p => p.IsReady)
                    .EndGroup()
                .EndGroupContainer();

            builder.Property(p => p.OrderId).DisplayName("Id").NotAutoGenerated().NotEditable();
            builder.Property(p => p.ProductCategory).DisplayName("Category").NotEditable();
            builder.Property(p => p.ProductName).DisplayName("Name").NotEditable();
            builder.Property(p => p.CustomerName).DisplayName("Customer").NotEditable();
            builder.Property(p => p.OrderDate).DisplayName("Date");
            builder.Property(p => p.Price).CurrencyDataType();
            builder.Property(p => p.IsReady).DisplayName("Is ready");
        }
    }
}
