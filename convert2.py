import json
from openpyxl import Workbook

# Load SARIF file
with open('scan-results.sarif', 'r', encoding='utf-8') as f:
    sarif = json.load(f)

# Ambil hasil dari SARIF
results = sarif['runs'][0].get('results', [])

# Buat workbook Excel
wb = Workbook()
ws = wb.active
if ws is not None:
    ws.title = "CodeQL Results"
    ws.append(['Rule ID', 'Message', 'File', 'Line'])

    for result in results:
        ruleId = result.get('ruleId', '')
        message = result['message']['text']
        location = result['locations'][0]['physicalLocation']
        file = location['artifactLocation']['uri']
        line = location['region']['startLine']
        ws.append([ruleId, message, file, line])

    # Simpan hasil sebagai file Excel
    wb.save('codeql_results.xlsx')
else:
    print("Gagal membuat worksheet. Workbook tidak valid.")
