import msvcrt
import time
import os
import ctypes
import numpy as np

background = np.array([[1,1,1,1,1,1,1,1,1,1,1,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,0,0,0,0,0,0,0,0,0,0,1],
                       [1,1,1,1,1,0,0,0,1,1,1,1],
                       [1,1,1,1,1,1,1,0,1,1,1,1],
                       [1,1,1,1,1,1,1,1,1,1,1,1]])

count = 0
x, y = 3, 3
rotate = 0
block_num = 0
def cls():
    os.system('cls')

def gotoxy(x, y):
   ctypes.windll.kernel32.SetConsoleCursorPosition(ctypes.windll.kernel32.GetStdHandle(-11), (((y&0xFFFF)<<0x10)|(x&0xFFFF)))

# block_L = np.array([[0, 0, 0, 0],
#                     [0, 1, 0, 0], 
#                     [0, 1, 1, 1], 
#                     [0, 0, 0, 0]])
block = np.array([[[[0, 0, 0, 0],
                    [0, 1, 0, 0], 
                    [0, 1, 1, 1], 
                    [0, 0, 0, 0]],
                   [[0, 0, 0, 0],
                    [0, 1, 1, 0], 
                    [0, 1, 0, 0], 
                    [0, 1, 0, 0]],
                   [[0, 0, 0, 0],
                    [0, 1, 1, 1], 
                    [0, 0, 0, 1], 
                    [0, 0, 0, 0]],
                   [[0, 0, 1, 0],
                    [0, 0, 1, 0], 
                    [0, 1, 1, 0], 
                    [0, 0, 0, 0]]],
                    
                  [[[0, 0, 0, 0],
                    [0, 1, 1, 0], 
                    [0, 1, 1, 0], 
                    [0, 0, 0, 0]],
                   [[0, 0, 0, 0],
                    [0, 1, 1, 0], 
                    [0, 1, 1, 0], 
                    [0, 0, 0, 0]],
                   [[0, 0, 0, 0],
                    [0, 1, 1, 0], 
                    [0, 1, 1, 0], 
                    [0, 0, 0, 0]],
                   [[0, 0, 0, 0],
                    [0, 1, 1, 0], 
                    [0, 1, 1, 0], 
                    [0, 0, 0, 0]]]
                    ])


def make_background():
    for j in range(22):
        for i in range(12):
            if background[j][i] == 1:
                gotoxy(i, j)
                print("*")
            else :
                gotoxy(i, j)
                print("-")

def make_background_value():
    for j in range(22):
        for i in range(12):
            if background[j][i] == 1:
                gotoxy(i + 15, j)
                print("1")
            else :
                gotoxy(i + 15, j)
                print("0")

def make_block():
    for j in range(4) :
        for i in range(4):
            
            if block[block_num][rotate][j][i] == 1 : 
                gotoxy(i + x, j + y)
                print("*")
def line_check(line_num):
    block_count = 0
    for i in range(1, 11):
        if background[line_num,i] == 1:
            block_count +=1
            
    if block_count == 10:
        for j  in range(19):
            for i in range(10):
                background[20-j, i+1] = background[19-j, i+1]
        make_background()
        make_background_value()


def delete_block():
    for j in range(4) :
        for i in range(4):
            
            if block[block_num][rotate][j][i] == 1 : 
                gotoxy(i + x, j + y)
                print("-")
            
def overlap_check(offset_x, offset_y) :
    block_count = 0
    
    for j in range(4) :
        for i in range(4):
            
            if block[block_num][rotate][j][i] == 1 and background[j + y + offset_y , i + x + offset_x] == 1: 
                block_count += 1
    return block_count

def overlap_check_rotate() :
    block_count = 0
    dummy_rotate = rotate
    dummy_rotate += 1
    if dummy_rotate == 4:
        dummy_rotate = 0
    for j in range(4) :
        for i in range(4):
            
            if block[block_num][dummy_rotate][j][i] == 1 and background[j + y, i + x] == 1: 
                block_count += 1
    return block_count

def insert_block() :
    
    for j in range(4) :
        for i in range(4):
            
            if block[block_num][rotate][j][i] == 1: 
                background[j + y, i + x] = 1
    
    
make_background()
make_background_value()
while(1):
    
    if msvcrt.kbhit() :
        key = msvcrt.getch()
        
        if key == b'a' :
            if overlap_check(-1, 0) == 0:
                delete_block()
                x-= 1
                make_block()
        elif key == b'd' :
            if overlap_check(1, 0) == 0 :
                delete_block()
                x += 1
                make_block()
        elif key == b's' :
            if overlap_check(0, 1) == 0:
                delete_block()
                y += 1
                make_block()
        elif key == b'r' :
            if overlap_check_rotate() == 0:
                delete_block() 
                rotate += 1
                if rotate == 4:
                    rotate = 0
                make_block()
 
    if count == 100 :
        count = 0
        
        if overlap_check(0, 1) == 0 :
            delete_block()
            y += 1
            make_block()
        else :
            insert_block()
            make_background_value()
            x = 3
            y = 3
            for i  in range(1, 21):
                line_check(i)
            
            block_num += 1
            if block_num > 1:
                block_num = 0
    count += 1
    time.sleep(0.01)