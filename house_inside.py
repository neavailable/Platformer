import pygame
from hernia_vsiaka import house
from access_import import *
from all_objects import *
from main_level import *
class House():
    def __init__(self, surf, create_lvl):
        self.create_lvl = create_lvl
        self.display_surf = surf
        dungeon_timber_brace_3_layout = import_csv(house['dungeon_timber_brace_3'])
        self.dungeon_timber_brace_3_sprites = self.create_house(dungeon_timber_brace_3_layout, 'dungeon_timber_brace_3')
    def create_house(self, layout, type):
        sprite_group = pygame.sprite.Group()
        for row_ind, row in enumerate(layout):
            for col_ind, val in enumerate(row):
                if val != '-1':
                    x = col_ind * 32
                    y = row_ind * 32
                    if type == 'dungeon_timber_brace_3':
                        dungeon_timber_brace_3_tile_list = import_cut_graphics('../../Platformer images/objects/dungeon_timber_brace_3.png', 83, 17)
                        dungeon_timber_brace_3_surface = dungeon_timber_brace_3_tile_list[int(val)]
                        sprite = StaticTile(83, 17, x, y, dungeon_timber_brace_3_surface)
                        sprite_group.add(sprite)
        return sprite_group
    def input(self):
        keys = pygame.key.get_pressed()
        if keys[pygame.K_ESCAPE]:
            self.create_lvl(0, 1216, 130)

    def run(self):
        self.input()
        self.dungeon_timber_brace_3_sprites.draw(self.display_surf)
