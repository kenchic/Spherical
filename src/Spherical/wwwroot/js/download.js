function downloadFile(fileName, mimeType, bytes) {
  try {
    const blob = new Blob([new Uint8Array(bytes)], { type: mimeType || 'application/octet-stream' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName || 'download';
    document.body.appendChild(a);
    a.click();
    a.remove();
    URL.revokeObjectURL(url);
  } catch (e) {
    console.error('downloadFile error', e);
  }
}

