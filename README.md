# Robocode Tank Royale Bot
Tugas Besar 1 IF2211 Strategi Algoritma -  Greedy <br>
Robocode Tank Royale github : https://github.com/robocode-dev/tank-royale

<p align="center">
  <img height="360px" src="https://i.ibb.co.com/7tCVVnHS/1742800772597.jpg" alt="foto"/>
  <br>
  <a><i><sup>Kelompok "Bakwan Jagung"</sup></i></a>
</p>

## Anggota 
1. Amira Izani (13523143)
2. Frederiko Eldad Mugiyono (13523147)
3. Natalia Desiany Nursimin (13523157)

## Deskripsi Singkat
Program ini merupakan implementasi bot dengan algoritma Greedy yang telah dibuat. Algoritma greedy yang telah diimplementasikan berbeda pada setiap bot:
- **Bot Ako** berfokus pada pergerakan agresif dan eksekusi tembakan langsung terhadap target yang paling mudah diserang.
- **Bot Handoko** memilih target dengan jarak terdekat dan menyerang secara agresif saat terjadi interaksi langsung dengan musuh.
- **Bot Amethyst** menerapkan strategi “Opportunistic Survivalist” dengan menghindari pertempuran di awal hingga menemukan peluang untuk melakukan serangan agresif berupa ramming.
- **Bot KakureMomojiri** mengutamakan pergerakan adaptif, menghindari dinding, serta merespons serangan dengan manuver defensif sambil tetap mencari peluang menyerang lawan.

## Cara menjalankan Game Engine 💻
### Requirement
- Java(TM) Runtime Environment (https://www.java.com/en/) 

### Instalasi dan konfigurasi awal
1. Download source code (.zip) pada release terbaru game engine (https://github.com/Ariel-HS/tubes1-if2211-starter-pack)
2. Extract zip tersebut, lalu masuk ke folder hasil extractnya dan buka terminal
3. Masuk ke root directory dari project (sesuaikan dengan nama rilis terbaru)
```sh
cd tubes1-if2211-starter-pack-1.0
```
4. Jalankan file .jar game engine 
```sh
java -jar robocode-tankroyale-gui-0.30.0.jar
```

## Cara menjalankan Bot 🤖

### Requirement
- DotNet (https://dotnet.microsoft.com/en-us/download) 

### Instalasi
1. Clone repository
```sh
git clone https://github.com/susTuna/Tubes1_Bakwan-Jagung
```

### Menjalankan Bot
1. Buka game engine klik tombol "Config"
2. Klik tombol "Bot Root Directories"
3. Masukkan *directory* dari repository yang sudah di clone
4. Klik tombol "Battle"
5. Klik tombol "Start Battle"
6. Boot bot yang ingin dimainkan
7. Add bot ke dalam permainan setelah di boot
8. Mulai permainan dengan menekan tombol "Start Battle"