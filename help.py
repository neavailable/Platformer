from os import walk
import pygame
def import_folder(path):
    list = []
    for a, b, main_information in walk(path):
        for image in main_information:
            folder = path + '/' + image
            img_surf = pygame.image.load(folder).convert_alpha()
            list.append(img_surf)

    return list
