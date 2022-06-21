from csv import reader
from os import walk
import pygame
def import_csv(path):
    with open(path) as map:
        terrain_map = []
        level = reader(map, delimiter = ',')
        for row in level:
            terrain_map.append(list(row))
        return terrain_map
def import_cut_graphics(path, tile_size_x, tile_size_y):
    surface = pygame.image.load(path).convert_alpha()
    tile_num_x = int(surface.get_size()[0] / tile_size_x)
    tile_num_y = int(surface.get_size()[1] / tile_size_y)
    cut_tiles = []
    for row in range(tile_num_y):
        for col in range(tile_num_x):
            x = col * tile_size_x
            y = row * tile_size_y
            new_surf = pygame.Surface((tile_size_x, tile_size_y), flags = pygame.SRCALPHA).convert_alpha()
            new_surf.blit(surface, (0, 0), pygame.Rect(x, y, tile_size_x, tile_size_y))
            cut_tiles.append(new_surf)
    return cut_tiles
def folder(path):
    surf_list = []
    for a, b, main_info in walk(path):
        for images in main_info:
            full_path = path + '/' + images
            surf = pygame.image.load(full_path).convert_alpha()
            surf_list.append(surf)
    return surf_list
