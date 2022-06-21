import pygame
from hernia_vsiaka import *
import time
class Node(pygame.sprite.Sprite):
    def __init__(self, pos, status, path):
        super().__init__()
        if status == 'available':
            self.status = 'available'
            self.image = pygame.image.load(path)
        else:
            self.status = 'ne_available'
            self.image = pygame.image.load('D:\Python Project\Pygame project\Platformer images\other/lock.jpg')
        self.rect = self.image.get_rect(center = pos)
    def animation(self):
        self.index += 0.1
        if self.index >= len(self.frames):
            self.index = 0
        self.image = self.frames[int(self.index)]
    def run(self):
        self.animation()


class Rect(pygame.sprite.Sprite):
    def __init__(self, pos):
        super().__init__()
        self.image = pygame.image.load('D:\Python Project\Pygame project\Platformer images\other/frame.jpg').convert()
        self.rect = self.image.get_rect(center = pos)

class Overworld():
    def __init__(self, start_level, max_level, surface, create_lvl):
        self.max_level = max_level
        self.display_surf = surface
        self.current_level = start_level
        self.create_lvl = create_lvl
        self.max_level = max_level
        self.setup_lvl()
        self.draw_rect()

    def setup_lvl(self):
        self.nodes_sprites = pygame.sprite.Group()
        for index, data in enumerate(all_levels.values()):
            if index < self.max_level:
                node_sprite = Node(data['stage_pos'], 'available', data['node_picture'])
            else:
                node_sprite = Node(data['stage_pos'], 'ne_available', data['node_picture'])
            self.nodes_sprites.add(node_sprite)
    def draw_rect(self):
        self.rects_sprites = pygame.sprite.GroupSingle()
        rect1 = Rect(self.nodes_sprites.sprites()[self.current_level].rect.center)
        self.rects_sprites.add(rect1)
    def input(self):
        keys = pygame.key.get_pressed()
        if keys[pygame.K_UP] and self.current_level > 0:
            self.current_level -= 1
            time.sleep(0.2)
        elif keys[pygame.K_DOWN] and self.current_level < self.max_level - 1:
            self.current_level += 1
            time.sleep(0.2)
        if keys[pygame.K_RETURN]:
            self.create_lvl(self.current_level, 100, 430)
    def update_icon_pos(self):
        self.rects_sprites.sprite.rect.bottom = self.nodes_sprites.sprites()[self.current_level].rect.bottom + 3.8
    def run(self):
        self.input()
        self.update_icon_pos()
        self.rects_sprites.draw(self.display_surf)
        self.nodes_sprites.draw(self.display_surf)
