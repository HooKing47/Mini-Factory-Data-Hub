import requests
import time
import random
import urllib3
from datetime import datetime

# ปิดการแจ้งเตือนเรื่อง SSL Certificate (เพราะเป็น localhost)
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

# ชี้ไปที่ Port ของ API ที่ดึงมาจาก launchSettings.json (ใช้ HTTPS)
API_URL = "http://localhost:5209/api/machinedata"

machine_ids = ["M-01", "M-02", "M-03"]

print(f"Starting Factory Simulator...")
print(f"Targeting API: {API_URL}")

while True:
    machine_id = random.choice(machine_ids)
    temperature = round(random.uniform(40.0, 95.0), 2)
    
    status = "Warning" if temperature > 85.0 else "Normal"

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
