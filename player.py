import pygame
from help import import_folder
class Player(pygame.sprite.Sprite):
    def __init__(self, pos):
        super().__init__()
        self.all_import()
        self.index = 0
        self.animate_speed = 0.15
        self.image = self.animations['idle'][self.index]
        self.rect = self.image.get_rect(topleft = pos)
        self.direction = pygame.math.Vector2(0,0)
        self.jump_speed = -15
        self.gravity = 0.8
        self.face = True
        self.status = 'idle'
        self.move1 = 8
        self.on_ground = False
        self.on_ceiling = False
        self.on_left = False
        self.on_right = False
    def movement(self):
         keys = pygame.key.get_pressed()
         if keys[pygame.K_RIGHT]:
             self.face = True
             self.direction.x = 1
         elif keys[pygame.K_LEFT]:
             self.face = False
             self.direction.x = -1
         else:
             self.direction.x = 0
         if keys[pygame.K_UP] and self.on_ground:
             self.on_ground  = False
             self.jump()
    def all_import(self):
        heroe_path = 'D:/Python Project/Pygame project/Platformer images/main_heroe/'
        self.animations = {'idle':[], 'run':[], 'jump':[], 'fall':[]}
        for animation in self.animations.keys():
            full_path = heroe_path + animation
            self.animations[animation] = import_folder(full_path)
    def get_status(self):
        if self.direction.y < 0:
            self.status = 'jump'
        elif self.direction.y > 1:
            self.status = 'fall'
        else:
            if self.direction.x == 0:
                self.status = 'idle'
            else:
                self.status = 'run'
    def animate(self):
        animation = self.animations[self.status]
        self.index += self.animate_speed
        if self.index >= len(animation):
            self.index = 0
        if self.face:
            self.image = animation[int(self.index)]
        else:
            self.image = pygame.transform.flip(animation[int(self.index)], True, False)
        if self.on_ground and self.on_right:
            self.rect = self.image.get_rect(bottomright = self.rect.bottomright)
        elif self.on_ground and self.on_left:
            self.rect = self.image.get_rect(bottomleft = self.rect.bottomleft)
        elif self.on_ground:
            self.rect = self.image.get_rect(midbottom = self.rect.midbottom)
        elif self.on_ceiling and self.on_right:
            self.rect = self.image.get_rect(topright = self.rect.topright)
        elif self.on_ceiling and self.on_left:
            self.rect = self.image.get_rect(topleft = self.rect.topleft)
        elif self.on_ceiling:
            self.rect = self.image.get_rect(midtop = self.rect.midtop)

    def jump(self):
        self.direction.y = self.jump_speed
    def apply_gravity(self):
        self.direction.y += self.gravity
        self.rect.y += self.direction.y
    def update(self):
        self.movement()
        self.animate()
        self.get_status()
        self.rect.x += self.direction.x * self.move1
