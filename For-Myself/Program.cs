var list1 = new List<int> { 1,2,3,4,5,6};
//! Here we are writing kind of a func that adds up all the values that are in a List
//! We can also perform Aggregate func over strings as well;
Console.WriteLine(list1.Aggregate((a,b) => a + b));

//! Distinct Removes duplicate values
List<int> list2 = new List<int> { 1, 2, 4, 5, 7, 8 ,7,7,8};
Console.WriteLine(string.Join(", ",list2.Distinct()));

//! Intersect Returns values that both lists have
Console.WriteLine(string.Join(", ",list1.Intersect(list2)));