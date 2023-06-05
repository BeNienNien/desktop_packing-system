using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingMachine.core.Domain.Model
{
    public class Ingredient
    {
        public int _Cardinal { get; set; }
        public string _IngredientID { get; set; }
        public string _IngredientName { get; set; }
        public Ingredient(int cardinal, string ingredientID, string ingredientname)
        {
            _Cardinal = cardinal;
            _IngredientID = ingredientID;
            _IngredientName = ingredientname;
        }
    }
}
