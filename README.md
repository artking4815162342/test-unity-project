# test-unity-project

Тестовый проект, выполненный по заданию:

-Локация- Простая локация: меш/скайбокс· 
По локации разбросаны гранаты разных(можно кубики разных цветов сразным цветом взрыва) типов· 
По локации в рандомных местах расставлены Болванки врагов.-Персонаж-· Игрок управляет Персонажем. Персонаж может:  

- Двигаться по WASD  
- Кидать гранату по ЛКМ  
- Подбирать разбросанные гранаты  
- Переключать тип гранат

- процесс кидания гранаты (если у персонажа они есть) состоит из двухэтапов:  
  1) При нажатии ЛКМ персонаж замирает, появляется прицел идвижением мыши настраивается точка попадания гранаты. 
  Граната летит побаллистической траектории, которую нужно отрисовывать в процессеприцеливания. 
  
  2) При отжатии ЛКМ граната летит по заданной траектории и взрывается при попадании в объект. 

- Игрок подбирает гранаты при подходе к ним ближе чем на N.
- Переключение гранат по стрелкам на клавиатуре.

-UI- Нужно видеть сколько у игрока гранат разных типов.

-Враги- Враги стоят неподвижно· У врага есть ХП· При взрыве гранаты в радиусе K от врага у него отнимается X хп 
(нужноотобразить визуально сколько отнялось, используя TextMeshPro)· Когда хп становится <= 0 враг умирает.

Бонусное задание:
У игрока есть ХП, у мобов есть ИИ и они могут атаковать врага (также гранатами).