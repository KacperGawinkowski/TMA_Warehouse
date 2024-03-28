using AntDesign;

namespace TMA_Warehouse.Client.Shared
{
    public static class Rules
    {
        public static FormValidationRule[] RuleRequired = new FormValidationRule[] { new FormValidationRule { Required = true, Message = "Field is required" } };
        public static FormValidationRule[] MoneyRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };
        public static FormValidationRule[] QuantityRule = new FormValidationRule[] { new FormValidationRule { Required = true, Type = FormFieldType.Number, Min = 0.0001m } };
    }
}
