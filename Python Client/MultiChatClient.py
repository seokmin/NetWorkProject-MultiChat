#-*- coding: utf-8 -*-

from socket import *
from select import select
import pyDes
import base64
import sys
#from Crypto.Cipher import DES

port = 9282
BUFSIZE = 1024
addr = ('',0)

class DES:
    def __init__(self, iv, key):
        self.iv = iv
        self.key = key
    def encrypt(self, data):
        k = pyDes.des(self.key, pyDes.ECB, self.iv, pad=None, padmode=pyDes.PAD_PKCS5)
        d = k.encrypt(data)
        d = base64.encodestring(d)
        return d
    def decrypt(self, data):
        k = pyDes.des(self.key, pyDes.ECB, self.iv, pad=None, padmode=pyDes.PAD_PKCS5)
        data = base64.decodestring(data)
        d = k.decrypt(data)
        return d

def validate_ip(s):
    a = s.split('.')
    if len(a) != 4:
        return False
    for x in a:
        if not x.isdigit():
            return False
        i = int(x)
        if i < 0 or i > 255:
            return False
    return True

def try_connect():
    addr = (hostIP,port)
    try:
        client.connect(addr)
    except Exception as e:
        print ('서버(%s:%s)에 연결할 수 없습니다.' % addr)
        return False
    print ('서버(%s:%s)에 접속 성공.' % addr)
    return True

print '단톡 - python version'
print 'This program provides client mode only.'
print ''

nick = raw_input('이름을 입력해 주세요 : ')

client = socket(AF_INET, SOCK_STREAM)

iv = base64.b16decode('1122334455667788')
key = iv
des = DES(iv,key)

while True:
    hostIP =raw_input("호스트의 IP주소를 입력하세요. (ex|127.0.0.1) : ")
    if validate_ip(hostIP):
        if try_connect(): 
            break
    else:
        print '잘못 입력하셨습니다. 다시 입력해 주세요.'

def refreshNames(names):
    for i in names.split('ㅫ'):
        if len(i) > 0:
            print(i + ', ')


def printOut(msg):
    msg = des.decrypt(msg)
    msg = msg.decode("cp949")
    ##msg = unicode(msg)
    msg = msg.split('ㅩ'.decode('cp949'))
    print msg
    refreshNames(msg[0])
    if 'ㅴ' in msg[1]:
        msg = msg[1].split('ㅴ')
        print ('%s님께서 입장하셨습니다.' %msg[0])
    if len(msg[1]) > 0 :
        print('%s'%msg)
    prompt()



def prompt():
    sys.stdout.flush()


while True:
    try:
        connection_list = [sys.stdin, client]

        read_socket, write_socket, error_socket = select(connection_list, [], [], 10)
        for sock in read_socket:
            if sock == client:
                data = sock.recv(BUFSIZE)
                if not data:
                    print('채팅 서버(%s:%s)와의 연결이 끊어졌습니다.' %addr)
                    print('접속을 종료합니다.')
                    client.close()
                    sys.exit()
                else:
                    #수신부
                    printOut(data)
            else:
                #송신부
                message = sys.stdin.readline()
                message = nick + '>>' + message
                message = des.encrypt(message)
                #message = 'a'
                print ('send : %s' %message)
                client.sendall(message)
                prompt()

    except KeyboardInterrupt:
        client.close()
        sys.exit()
