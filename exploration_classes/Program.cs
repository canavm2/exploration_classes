using FileTools;
using People;
using Company;


FileTool fileTool = new FileTool();
Console.WriteLine($"The current index is: {fileTool.ReadIndex()}");
IndexId index = new IndexId(fileTool.ReadIndex());
//RelationshipCache relationshipcache = fileTool.ReadRelationshipCache("relationships");
//ModifierList modifierlist = new ModifierList();
//ModifierList modifierlist = fileTool.ReadModifierList("modifierlist");
//TraitList traitlist = new();
TraitList traitlist = fileTool.ReadTraitList("traitlist");
PlayerCompany testcompany = fileTool.ReadCompany("company");
Console.WriteLine(testcompany.Describe());
CitizenCache citizens = fileTool.ReadCitizens("citizens");

#region createcitizens
//CitizenCache citizens = new CitizenCache(index, 100);
//Console.WriteLine($"femalecitizens has: {citizens.FemaleCitizens.Count} items.\nThe first female is:\n{citizens.FemaleCitizens[0].DescribeCitizen()}");
//Console.WriteLine($"malecitizens has: {citizens.MaleCitizens.Count} items.\nThe first male is:\n{citizens.MaleCitizens[0].DescribeCitizen()}");
//Console.WriteLine($"nbcitizens has: {citizens.NBCitizens.Count} items.\nThe first non-binary is:\n{citizens.NBCitizens[0].DescribeCitizen()}");
#endregion

#region testingmodifiers
//Modifier testmodifier = new("tired", "battle", "stat", "str", -10, true, 1000, "Battle Fatigue");
//testcompany.Advisors["master"].AddModifier(testmodifier);
//Console.WriteLine(testcompany.Advisors["master"].DescribeCitizen());
//testcompany.Advisors["master"].RemoveModifier("tired");
//Console.WriteLine(testcompany.Advisors["master"].DescribeCitizen());
#endregion

#region createcompany
//List<Citizen> advisors = new List<Citizen>();
//for (int i = 0; i < 7; i++)
//    advisors.Add(citizens.GetRandomCitizen());
//Citizen master = citizens.GetRandomCitizen();
//PlayerCompany testcompany = new("testcompany", index, master, advisors);
#endregion

#region readcompany
//Relationships.UpdateRelationships(testcompany,relationshipcache);
#endregion

#region advisortesting
//Citizen replacementadvisor = citizens.GetRandomCitizen();
//Console.WriteLine(replacementadvisor.Describe());
//Relationships.ReplaceAdvisor(replacementadvisor, testcompany, "advisor1", citizens, relationshipcache);
#endregion

//foreach (Citizen.Trait trait in traitlist.Traits.Values)
//{
//    Console.WriteLine(trait.Summary());
//}
//foreach (Citizen.Trait trait in traitlist.Traits.Values)
//{
//    testcompany.Advisors["master"].RemoveTrait(trait.Name);
//}


//Stores everything again
index.StoreIndex(fileTool);
fileTool.StoreCitizens(citizens, "citizens");
fileTool.StoreCompany(testcompany, "company");
//fileTool.StoreModifierList(modifierlist, "modifierlist");
fileTool.StoreTraitList(traitlist, "traitlist");
//fileTool.StoreRelationshipCache(relationshipcache, "relationships");