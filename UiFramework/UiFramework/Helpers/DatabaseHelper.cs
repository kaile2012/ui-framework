//using System;
//using System.Text;
//using UiFramework.V2.Interfaces;

//namespace UiFramework.V2.Helpers
//{
//    // Shouldn't be needed

//    public static class DatabaseHelper
//    {
//        public static char SerialisedSeperatorCharacter_Objects { get; set; } = '`';

//        public static char SerialisedSeperatorCharacter_Properties { get; set; } = '¬';

//        public static string Serialise(this ILayoutItem[] layoutItems)
//        {
//            StringBuilder sb = new StringBuilder();

//            foreach (ILayoutItem item in layoutItems)
//            {
//                sb.Append(item);
//                sb.Append(SerialisedSeperatorCharacter_Properties);
//                sb.Append(SerialisedSeperatorCharacter_Objects);
//            }

//            return sb.ToString();
//        }

//        public static ILayoutItem[] Deserialise(this string items)
//        {
//            return null;
//        }
//    }
//}
