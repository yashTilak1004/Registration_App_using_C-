import os
from PIL import Image
from pytesseract import pytesseract
from pypdf import PdfReader
from pdf2image import convert_from_path
import time
start = time.time()
# Path to the Tesseract executable
path_to_tesseract = r"C:\\Users\\Saivishal.Vempati\\AppData\\Local\\Programs\\Tesseract-OCR\\tesseract.exe"
pytesseract.tesseract_cmd = path_to_tesseract
pdf_directory = 'pdf_files'
for filename in os.listdir(pdf_directory):
    if filename.endswith('.pdf'):
        pdf_path = os.path.join(pdf_directory, filename)
        print(f"Processing: {pdf_path}")
        reader = PdfReader(pdf_path)
        box = reader.pages[0].mediabox
        print(f"Dimensions (Width x Height): {box.width} x {box.height} points")
        images = convert_from_path(pdf_path, poppler_path="poppler-24.07.0\\Library\\bin")
        full_text = ""
        for img in images:
            text = pytesseract.image_to_string(img)
            full_text += text + "\n"
        output_filename = os.path.splitext(filename)[0] + '_output.txt'
        output_path = os.path.join(pdf_directory, output_filename)
        with open(output_path, 'w', encoding='utf-8') as file:
            file.write(full_text)
        print(f"Extracted text saved to: {output_path}")
end = time.time()
print(end - start)
print(f"process is done in {end-start} seconds")
