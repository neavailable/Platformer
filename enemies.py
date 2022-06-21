import pygame
from all_objects import AnimatedTile

class Enemies(AnimatedTile):
    def __init__(self, w, h, x, y, path):
        super().__init__(w, h, x, y, path)
        self.run = 3
    def move(self):
        self.rect.x += self.run
    def flip(self):
        if self.run < 0:
            self.image = pygame.transform.flip(self.image, True, False)
    def reversed(self):
        self.run *= -1
    def update(self, shift):
        self.rect.x += shift
        self.animation()
        self.move()
        self.flip()