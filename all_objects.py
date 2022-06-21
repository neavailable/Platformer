import pygame
from access_import import *
class Tile(pygame.sprite.Sprite):
	def __init__(self, w, h, x, y):
		super().__init__()
		self.image = pygame.Surface((w, h))
		self.rect = self.image.get_rect(topleft = (x,y))

	def update(self,shift):
		self.rect.x += shift

class StaticTile(Tile):
	def __init__(self,w, h, x, y, surface):
		super().__init__(w, h, x, y)
		self.image = surface

class SingleStaticTile(StaticTile):
    def __init__(self, w, h, x, y, path):
        super().__init__(w, h, x, y, pygame.image.load(path).convert_alpha())


class AnimatedTile(Tile):
    def __init__(self, w, h, x, y, path):
        super().__init__(w, h, x, y)
        self.frames = folder(path)
        self.index = 0
        self.image = self.frames[self.index]
    def animation(self):
        self.index += 0.2
        if self.index >= len(self.frames):
            self.index = 0
        self.image = self.frames[int(self.index)]
    def update(self, val):
        self.rect.x += val
        self.animation()
