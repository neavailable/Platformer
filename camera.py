import pygame
class Camera(pygame.sprite.Group):
    def __init__(self):
        super().__init__()
        self.display_surface = pygame.display.get_surface()
        self.direction = pygame.math.Vector2(800, 0)
    def custom_draw(self):
        for sprite in sorted(self.sprites(), key = lambda sprite: sprite.rect.centery):
            direction_position = sprete.rect.topleft + self.direction
            self.display_surface.blit(sprite.image, direction_position)
