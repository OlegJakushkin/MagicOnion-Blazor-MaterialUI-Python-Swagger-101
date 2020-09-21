#script to check Clien-Server protocol without VM or Display
import time

from bravado.client import SwaggerClient
from bravado.config import CONFIG_DEFAULTS
from bravado.swagger_model import load_file
import os.path
import json
def run():
    config = CONFIG_DEFAULTS.copy()
    config['validate_swagger_spec'] = False
    client = SwaggerClient.from_url('http://localhost:9999/swagger.json', config=config)

    key_val = ""
    key_path = './key'
    if not os.path.isfile(key_path) or  os.path.getsize(key_path) <= 0:
        key = client.IHardwareBoxService.post_IHardwareBoxService_InitHardware().response()
        key_val = key.incoming_response.text
        key_file = open(key_path, 'w')
        arr = []
        arr.append(key_val+ "\n")
        key_file.writelines(arr)
        key_file.close()
    else:
        key_file = open(key_path, 'r')
        key_val = key_file.readline().replace("\n", '')
        key_file.close()

    print(key_val)
    status_val = '"StandBy"'
    while True:
        status = client.IHardwareBoxService.post_IHardwareBoxService_UpdateStatus(code='{"Code":"Ok","HardwareId":'+key_val+'}').response()
        status_val = status.incoming_response.text
        print(status_val)
        if status_val != '"StandBy"':
            print("> Selling item!")
            vend =  client.IHardwareBoxService.post_IHardwareBoxService_BeginServeRequest(HardwareId=key_val.replace("\"", '').replace("\n", '')).response()
            vend_obj = json.loads(vend.incoming_response.raw_bytes)
            if not vend_obj['IsCanceled']:
                pos = vend_obj['Position']
                print(pos)
                sold =  client.IHardwareBoxService.post_IHardwareBoxService_EndServeRequest(code= '{"HardwareId":'+key_val+',"Code":"Ok","RequestId":"'+vend_obj['RequestId']+'"}').response()
                print("OK Sold item!")
            else:
                print("X Sell was canceled!")

        time.sleep(3)

# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    run()

