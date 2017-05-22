SELECT v.Name,
CASE
WHEN m.Name IS NULL THEN '0'
ELSE m.Name
END AS Name, 
m.Age
FROM Minions AS m
INNER JOIN MinionsVillains AS mv
ON m.Id = mv.MinionId
RIGHT OUTER JOIN Villains AS v
ON v.Id = mv.VillainId
WHERE v.Id = @VillainId



