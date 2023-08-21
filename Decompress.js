import pako from "pako";
import React, { useEffect, useState } from "react";

const Decompressdata = () => {
  const [dataDecompressed, setDataDecompressed] = useState("");
  let url = "https://localhost:7258/api/Home/";

  async function getUser() {
    await fetch("https://localhost:7258/api/Home", {
      method: "GET",
      headers: {
        Accept: "text/plain",
      },
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.text();
      })
      .then((data) => {
        setDataDecompressed(dataDecompress(data)); // The fetched text/plain response
      })
      .catch((error) => {
        console.error("Fetch error:", error);
      });
  }

  getUser();

  const dataDecompress = (jsoncompressed) => {
    console.log(jsoncompressed);
    // Decode base64 (convert ascii to binary)
    var strData = window.atob(jsoncompressed.replace(/"/g, ''));

    // Convert binary string to character-number array
    var charData = strData.split("").map((x) => {
      return x.charCodeAt(0);
    });

    // Turn number array into byte-array
    var binData = new Uint8Array(charData);

    // Pako
    var data = pako.inflate(binData);
    console.log(data);

    // Convert gunzipped byteArray back to ascii string:
    var jsonData = new TextDecoder("utf-8").decode(data);

    return jsonData;
  };
  // Get some base64 encoded binary data from the server. Imagine we got this:

  // Output to console
  return <div>{dataDecompressed}</div>;
};

export default Decompressdata;
