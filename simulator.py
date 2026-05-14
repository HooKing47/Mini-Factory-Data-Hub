import requests
import time
import random
import urllib3
from datetime import datetime

import os

# ปิดการแจ้งเตือนเรื่อง SSL Certificate (เพราะเป็น localhost)
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

# ชี้ไปที่ Port ของ API ที่ดึงมาจาก launchSettings.json หรือ Env Var
API_URL = os.getenv("API_URL", "http://localhost:5209/api/machinedata")

# เพิ่มเครื่องจักรเป็น 20 เครื่อง
machine_ids = [f"M-{str(i).zfill(2)}" for i in range(1, 21)]

while True:
    machine_id = random.choice(machine_ids)
    
    # มีโอกาส 5% ที่เครื่องจะพัง (อุณหภูมิพุ่งกระฉูด)
    if random.random() < 0.05:
        temperature = round(random.uniform(100.0, 150.0), 2)
        status = "Error"
    else:
        temperature = round(random.uniform(40.0, 85.0), 2)
        status = "Warning" if temperature > 80.0 else "Normal"


    data = {
        "MachineId": machine_id,
        "Temperature": temperature,
        "Status": status
    }

    try:
        # ยิง HTTP POST ไปที่ API พร้อมข้ามการเช็ค SSL (verify=False)
        response = requests.post(API_URL, json=data, verify=False)
        
        if response.status_code == 201:
            print(f"[{datetime.now().strftime('%H:%M:%S')}] Success! Sent: {data}")
        else:
            print(f"Failed. Status code: {response.status_code} - {response.text}")
            
    except requests.exceptions.ConnectionError:
        print("Error: Could not connect to API. Is 'dotnet run' running?")

    time.sleep(3)
