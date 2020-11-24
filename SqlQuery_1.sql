--it will show me how many people are from Bangalore, Mysore, Hyderabad, Vijayawada
Select City, count(*) 'Total Users' from Users  where City in('Bangalore','Mysore','Hyderabad','Vijayawada') group by City

--show only those cities which have more than 300 people in the contact list.
Select City, count(*) 'Total Users' from Users group by City HAVING count(*)>300