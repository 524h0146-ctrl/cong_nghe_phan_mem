"""Run after saving evidence: py -m pip install pymupdf; py BoSungMinhChung.py path/to/report.pdf Evidence"""
import sys
from pathlib import Path
import pymupdf as pdf
src, folder = map(Path,sys.argv[1:3])
doc = pdf.open(src)
toc = doc.get_toc()
files = sorted(f for f in folder.iterdir() if f.suffix.lower() in {".png",".jpg",".jpeg"})
if not files: raise SystemExit("No screenshot found")
for file in files:
    page = doc.new_page(width=842,height=595)
    page.insert_text((30,34),file.stem.encode("ascii","replace").decode(),fontsize=14)
    page.insert_image(pdf.Rect(25,50,817,565),filename=str(file),keep_proportion=True)
    toc.append([1,"Evidence - "+file.stem,len(doc)])
doc.set_toc(toc)
dest = src.with_name(src.stem+"_with_evidence.pdf")
doc.save(dest)
print(dest)
