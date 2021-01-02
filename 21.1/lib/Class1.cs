using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class Ingredients
{
    List<Food> foods;
    HashSet<string> ingredients = new HashSet<string>();
    HashSet<string> allergens = new HashSet<string>();
    public Ingredients(string input)
    {
        foods = input.Split("\r\n").Select(line => new Food(line)).ToList();
        foods.ForEach(food => food.allergens.ForEach(a => allergens.Add(a)));
        foods.ForEach(food => food.ingredients.ForEach(i => ingredients.Add(i)));

    }

    public long NotAllergic()
    {
        HashSet<string> possiblyAllergicIngredients = new HashSet<string>();
        // For each ingredient, could it be any of the allergens? If no, count it.
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
        var notAllergic = ingredients.Except(possiblyAllergicIngredients);
        var appearances = foods.Sum(food => food.ingredients.Sum(ing => notAllergic.Contains(ing) ? 1 : 0));
        return appearances;
    }
}

public class Food
{
    public List<string> ingredients;
    public List<string> allergens;
    public Food(string input)
    {
        var parts = input.Split(new string[] { " (contains ", ")" }, StringSplitOptions.None);
        ingredients = parts[0].Split(" ").ToList();
        allergens = parts[1].Split(", ").ToList();
    }
}


