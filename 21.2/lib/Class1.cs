using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class Ingredients
{
    List<Food> foods;
    SortedSet<string> ingredients = new SortedSet<string>();
    SortedSet<string> allergens = new SortedSet<string>();
    public Ingredients(string input)
    {
        foods = input.Split("\r\n").Select(line => new Food(line)).ToList();
        foods.ForEach(food => food.allergens.ForEach(a => allergens.Add(a)));
        foods.ForEach(food => food.ingredients.ForEach(i => ingredients.Add(i)));

    }

    public string AllergicIngredients()
    {
        EliminateNotAllergicIngredients();

        //Stack<Progress> todo = new Stack<Progress>();
        //todo.Push(new Progress());

        //while (todo.Count > 0)
        //{
        //    Progress progress = todo.Pop();
        //    Food nextFood = foods[progress.foodsProcessed];

        //    foreach (var ingredient in nextFood.ingredients)
        //    {
        //        foreach (var allergen in nextFood.allergens)
        //        {

        //        }
        //    }

        //}

        int[] allergensToIngredients = Enumerable.Range(0, allergens.Count).ToArray();
        int[] solution = null;
        int iperm = 0;
        foreach (var permutation in allergensToIngredients.Permutations())
        {
            // See if this configuration of allergens to ingredients works
            bool kosher = true;
            int c = permutation.Count();
            for (int i = 0; i < c; i++)
            {
                kosher = foods.Where(food => food.allergens.Contains(allergens.ElementAt(i)))
                    .All(food => food.ingredients.Contains(ingredients.ElementAt(permutation.ElementAt(i))));
                if (!kosher)
                    break;
            }
            iperm++;
            if (kosher)
                solution = permutation.ToArray();
        }

        var allergicIngs = solution.Aggregate(new StringBuilder(),
            (sb, i) => sb.Append($",{ingredients.ElementAt(i)}"), sb => sb.ToString(1, sb.Length - 1));
            

        return allergicIngs;
    }

    public void EliminateNotAllergicIngredients()
    {
        // For each ingredient, could it be any of the allergens? If no, count it.
        SortedSet<string> possiblyAllergicIngredients = new SortedSet<string>();
        foreach (var ing in ingredients)
        {
            foreach (var allergen in allergens)
            {
                // Does the above ingredient show up in ALL foods with this allergen?
                if (foods.Where(food => food.allergens.Contains(allergen))
                           .All(food => food.ingredients.Contains(ing)))
                    possiblyAllergicIngredients.Add(ing);
            }
        }
        ingredients = possiblyAllergicIngredients;
        foods.ForEach(food => {
            food.ingredients = food.ingredients.Intersect(ingredients).ToList();
        });
    }
}

public class Food
{
    public List<string> ingredients; // each item in this list MAY  correlate to one of the allergens
    public List<string> allergens;   // each item in this list MUST correlate to one of the ingredients
    public Food(string input)
    {
        var parts = input.Split(new string[] { " (contains ", ")" }, StringSplitOptions.None);
        ingredients = parts[0].Split(" ").OrderBy(i => i).ToList();
        allergens = parts[1].Split(", ").OrderBy(a => a).ToList();
    }
}

public class Progress
{
    public Dictionary<string, string> allergensToIngredients = new Dictionary<string, string>(); // [allergen] = ingredient
    public int foodsProcessed = 0;
    public Progress()
    {
    }
    public Progress(Progress prev)
    {

    }
}

public static class Extension
{
    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values) where T : IComparable<T>
    {
        if (values.Count() == 1)
            return new[] { values };
        return values.SelectMany(v => Permutations(values.Where(x => x.CompareTo(v) != 0)), (v, p) => p.Prepend(v));
    }
}