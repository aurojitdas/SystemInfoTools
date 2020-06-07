using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoTools
{
    class PerformanceCounterInfo
    {

        PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories();

        public void getCategoryInfo()
        {
          //  PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories();
            foreach (PerformanceCounterCategory category in categories)
            {
                Console.WriteLine("Category name: {0}", category.CategoryName);
                Console.WriteLine("Category type: {0}", category.CategoryType);
                Console.WriteLine("Category help: {0}", category.CategoryHelp);
                Console.WriteLine("===========================================");
            }
        }

        public void getCounterInfo(String category)
        {
            PerformanceCounterCategory categorie = new PerformanceCounterCategory(category);
            Console.WriteLine();

        }



}
}
