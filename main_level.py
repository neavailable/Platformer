import pygame
from access_import import import_cut_graphics, import_csv, folder
from all_objects import *
from enemies import *
from player import *
from overworld import *
from hernia_vsiaka import *
from house_inside import *
from camera import *
class Level:
    def __init__(self, current_level, surf, create_over, create_houses, player_x, player_y):
        #connecting
        self.create_over = create_over
        self.create_houses = create_houses
        self.current_lvl = current_level
        self.display_surf = surf
        self.current_x = 0
        self.player_x = player_x
        self.player_y = player_y
        self.offset = pygame.math.Vector2()
        level_data = all_levels[self.current_lvl]
        self.new_max_lvl = level_data['unlock']
        #player
        player_layout = import_csv(level_data['player'])
        self.player = pygame.sprite.GroupSingle()
        self.end_group = pygame.sprite.GroupSingle()
        self.player_settings(player_layout)

        #terrarian
        terrain_layout = import_csv(level_data['terrain'])
        self.terrain_sprites = self.create_map(terrain_layout, 'terrain')
        #enemies
        enemies_layout = import_csv(level_data['enemies'])
        self.enemies_sprites = self.create_map(enemies_layout, 'enemies')
        sharps_layout = import_csv(level_data['sharps'])
        self.sharps_sprites = self.create_map(sharps_layout, 'sharps')
        #other_objects
        boxes_layout = import_csv(level_data['boxes'])
        self.boxes_sprites = self.create_map(boxes_layout, 'boxes')
        #houses
        houses_layout = import_csv(level_data['houses'])
        self.houses_sprites = self.create_map(houses_layout, 'houses')
        #collidable door
        door_layout = import_csv(level_data['door'])
        self.door_sprites = self.create_map(door_layout, 'door')
        #block for enemies
        gavno_layout = import_csv(level_data['gavno'])
        self.gavno_sprites = self.create_map(gavno_layout, 'gavno')
        #block for player
        first_wall_layout = import_csv(level_data['first_wall'])
        self.first_wall_sprites = self.create_map(first_wall_layout, 'first_wall')
        ###
        self.camera_val1 = 0
    def colliding_house(self):
        keys = pygame.key.get_pressed()
        for sprite in self.door_sprites.sprites():
            if sprite.rect.colliderect(self.player.sprite.rect) and keys[pygame.K_SPACE]:
                self.create_houses()


    def create_map(self, layout, type):

        sprite_group = pygame.sprite.Group()
        for row_ind, row in enumerate(layout):
            for col_ind, val in enumerate(row):
                if val != '-1':
                    x = col_ind * 32
                    y = row_ind * 32 + 100
                    if type == 'terrain':
                        terrain_tile_list = import_cut_graphics('D:/Python project/Pygame project/Platformer images/cut_tilesets/all_cut_tilesets.png', 32, 32)
                        tile_surface = terrain_tile_list[int(val)]
                        sprite = StaticTile(32, 32, x, y, tile_surface)
                    if type == 'enemies':
                        y1 = row_ind * 32 + 76
                        sprite = Enemies(32, 32, x, y1, 'D:\Python project\Pygame project\Platformer images\enemies\goblin/run')
                    if type == 'sharps':
                        terrain_tile_list = import_cut_graphics('D:/Python project/Pygame project/Platformer images/cut_tilesets/all_cut_tilesets.png', 32, 32)
                        tile_surface = terrain_tile_list[int(val)]
                        sprite = StaticTile(32, 32, x, y, tile_surface)
                    if type == 'boxes':
                        sprite = SingleStaticTile(32, 32, x - 20, y - 6, 'D:\Python project\Pygame project\Platformer images\objects/chest1.png')
                    if type == 'houses':
                        houses_tile_list = import_cut_graphics('D:/Python project/Pygame project/Platformer images/objects/house_1.png', 251, 135)
                        houses_surface = houses_tile_list[int(val)]
                        sprite = StaticTile(251, 135, x, y - 103, houses_surface)
                    if type == 'door':
                        x1 = col_ind * 32
                        y1 = row_ind * 32 + 100
                        door_tile_list = import_cut_graphics('D:/Python project/Pygame project/Platformer images/objects/box_1/door.png', 32, 32)
                        door_surface = door_tile_list[int(val)]
                        sprite = StaticTile(5, 5, x1, y1, door_surface)
                    if type == 'gavno':
                        sprite = Tile(32, 32, x, y)
                    if type == 'first_wall':
                        sprite = Tile(32, 32, x, y)
                    sprite_group.add(sprite)
        return sprite_group
    def player_settings(self, layout):
        for row_ind, row in enumerate(layout):
            for col_ind, val in enumerate(row):
                if val == '0':
                    sprite = Player((self.player_x, self.player_y))
                    self.player.add(sprite)
                if val == '1':
                    y11 = y - 59
                    tile_surf = pygame.image.load('D:\Python Project\Pygame project\Platformer images/portal/portal.png').convert_alpha()
                    sprite = StaticTile(130, x, y11, tile_surf)
                    self.end_group.add(sprite)
    def width_touch(self):
        self.collidable = self.terrain_sprites.sprites() + self.first_wall_sprites.sprites()
        for sprite in self.collidable:
            if sprite.rect.colliderect(self.player.sprite.rect):
                if self.player.sprite.rect.x < 0:
                    self.player.sprite.rect.x = 0
                if self.player.sprite.direction.x < 0:
                    self.player.sprite.rect.left = sprite.rect.right
                    self.player.sprite.on_left = True
                    self.current_x = self.player.sprite.rect.left
                elif self.player.sprite.direction.x > 0:
                    self.player.sprite.rect.right = sprite.rect.left
                    self.player.sprite.on_right = True
                    self.current_x = self.player.sprite.rect.right
        if self.player.sprite.on_left and (self.player.sprite.rect.left < self.current_x or self.player.sprite.direction.x > 0):
            self.player.sprite.on_left = False
        if self.player.sprite.on_right and (self.player.sprite.rect.right > self.current_x or self.player.sprite.direction.x < 0):
            self.player.sprite.on_right = False
    def heigh_touch(self):
        self.player.sprite.apply_gravity()
        self.collidable = self.terrain_sprites.sprites() + self.first_wall_sprites.sprites()
        for sprite in self.collidable:
            if sprite.rect.colliderect(self.player.sprite.rect):
                if self.player.sprite.direction.y > 0:
                    self.player.sprite.direction.y = 0
                    self.player.sprite.on_ground = True
                    self.player.sprite.rect.bottom = sprite.rect.top
                elif self.player.sprite.direction.y < 0:
                    self.player.sprite.direction.y = 0
                    self.player.sprite.on_ceiling = True
                    self.player.sprite.rect.top = sprite.rect.bottom
        if self.player.sprite.on_ground and self.player.sprite.direction.y < 0 or self.player.sprite.direction.y > 1:
            self.player.sprite.on_ground = False
        if self.player.sprite.on_ceiling and self.player.sprite.direction.y > 0:
            self.player.sprite.on_ceiling = False
    def enemy_touch(self):
        for enemy in self.enemies_sprites.sprites():
            if pygame.sprite.spritecollide(enemy, self.gavno_sprites, False):
                enemy.reversed()
    def scroll_x(self):
        if self.player.sprite.rect.x < 300 and self.player.sprite.direction.x < 0:
            self.camera_val1 = 8
            self.player.sprite.move1 = 0
        elif self.player.sprite.rect.x > 600 and self.player.sprite.direction.x > 0:
            self.camera_val1 = -8
            self.player.sprite.move1 = 0
        else:
            self.camera_val1 = 0
            self.player.sprite.move1 = 8

    def death(self):
        self.enemies = self.enemies_sprites.sprites() + self.sharps_sprites.sprites()
        if pygame.sprite.spritecollide(self.player.sprite, self.enemies, False) or self.player.sprite.rect.y > 800:
            self.create_over(self.current_lvl, 1, 0)
    def check_win(self):
        if pygame.sprite.spritecollide(self.player.sprite,self.end_group, False):
            self.create_over(self.current_lvl, self.new_max_lvl, 0)
    def run(self):
        self.enemy_touch()
        #block for enemies
        self.gavno_sprites.update(self.camera_val1)
        #block for player
        self.first_wall_sprites.update(self.camera_val1)
        #terrain
        self.terrain_sprites.draw(self.display_surf)
        self.terrain_sprites.update(self.camera_val1)
        #objects
        self.boxes_sprites.update(self.camera_val1)
        self.houses_sprites.update(self.camera_val1)
        self.houses_sprites.draw(self.display_surf)
        self.door_sprites.update(self.camera_val1)
        #enemies
        self.enemies_sprites.draw(self.display_surf)
        self.enemies_sprites.update(self.camera_val1)
        self.sharps_sprites.update(self.camera_val1)
        #player
        self.player.update()
        self.player.draw(self.display_surf)
        #logic
        self.width_touch()
        self.heigh_touch()
        self.colliding_house()
        self.scroll_x()
        #connecting
        self.check_win()
        self.death()
