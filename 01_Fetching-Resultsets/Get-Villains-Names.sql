SELECT v.Name, COUNT(mv.MinionId) 
FROM Villains AS v
INNER JOIN MinionsVillains AS mv
ON v.Id = mv.VillainId
GROUP BY v.Name
HAVING COUNT(mv.MinionId) > 3
ORDER BY COUNT(mv.MinionId) DESC