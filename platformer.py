import pygame
from sys import exit
from game import *
from hernia_vsiaka import *
from camera import *
pygame.init()
sc = pygame.display.set_mode((1000, 600))
FPS = 60
cl = pygame.time.Clock()
game = Game(sc)
camera = Camera()
while True:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            exit()
    sc.fill((217, 217, 217))
    game.run()
    camera.update()
    pygame.display.update()
    cl.tick(FPS)

