SELECT v.Name, m.Name, m.Age
FROM Minions AS m
INNER JOIN MinionsVillains AS mv
ON m.Id = mv.MinionId
RIGHT OUTER JOIN Villains AS v
ON v.Id = mv.VillainId
WHERE v.Id = @VillainId



