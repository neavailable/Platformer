from overworld import *
from main_level import *
from house_inside import *
class Game:
    def __init__(self, surf):
        self.max_level = 1
        self.display_surf = surf
        self.status = 'overworld'
        self.overworld = Overworld(0, self.max_level, self.display_surf, self.create_lvl)
    def create_lvl(self, current_level, player_x, player_y):
        self.level = Level(current_level, self.display_surf, self.create_over, self.create_houses, player_x, player_y)
        self.status = 'level'
    def create_over(self, current_level, new_max_level, max_level):
        if new_max_level > max_level:
            max_level = new_max_level
        self.overworld = Overworld(current_level, self.max_level, self.display_surf, self.create_lvl)
        self.status = 'overworld'
    def create_houses(self):
        self.house = House(self.display_surf, self.create_lvl)
        self.status = 'house'
    def run(self):
        if self.status == 'overworld':
            self.overworld.run()
        elif self.status == 'house':
            self.house.run()
        else:
            self.level.run()
