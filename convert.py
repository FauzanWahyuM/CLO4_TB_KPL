import json
import csv

with open('scan-results.sarif', 'r', encoding='utf-8') as f:
    sarif = json.load(f)

results = sarif['runs'][0].get('results', [])
with open('codeql_results.csv', 'w', newline='', encoding='utf-8') as csvfile:
    fieldnames = ['Rule ID', 'Message', 'File', 'Line']
    writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
    writer.writeheader()

    for result in results:
        ruleId = result.get('ruleId', '')
        message = result['message']['text']
        location = result['locations'][0]['physicalLocation']
        file = location['artifactLocation']['uri']
        line = location['region']['startLine']
        writer.writerow({'Rule ID': ruleId, 'Message': message, 'File': file, 'Line': line})
