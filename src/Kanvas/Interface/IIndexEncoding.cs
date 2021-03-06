﻿using Kanvas.Models;
using Kanvas.Quantization.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kanvas.IndexEncoding.Models;

namespace Kanvas.Interface
{
    /// <summary>
    /// Describes methods to define an index based color encoding.
    /// </summary>
    public interface IIndexEncoding
    {
        /// <summary>
        /// The name of the index format.
        /// </summary>
        string FormatName { get; }

        /// <summary>
        /// Decodes image data to a list of colors.
        /// </summary>
        /// <param name="input">Index data to decode.</param>
        /// <returns>Decoded collection of indices.</returns>
        IEnumerable<IndexData> Load(byte[] input);

        /// <summary>
        /// Composes indices and a palette to a collection of colors.
        /// </summary>
        /// <param name="indices">Collection of <see cref="IndexData"/> to compose.</param>
        /// <param name="palette">Collection of colors to compose.</param>
        /// <returns>Composed collection of colors.</returns>
        IEnumerable<Color> Compose(IEnumerable<IndexData> indices, IList<Color> palette);

        /// <summary>
        /// Decomposes a collection of colors.
        /// </summary>
        /// <param name="colors">Collection of colors.</param>
        /// <returns>Decomposed collection of indices and palette.</returns>
        (IEnumerable<IndexData> indices, IList<Color> palette) Decompose(IEnumerable<Color> colors);

        /// <summary>
        /// Decomposes a collection of colors with a given palette.
        /// </summary>
        /// <param name="colors">Collection of colors.</param>
        /// <param name="palette">The palette to derive the indices from.</param>
        /// <returns>Decomposed collection of indices.</returns>
        IEnumerable<IndexData> DecomposeWithPalette(IEnumerable<Color> colors, IList<Color> palette);

        /// <summary>
        /// Quantizes a collection of colors.
        /// </summary>
        /// <param name="colors">Collection of colors.</param>
        /// <returns>Quantized collection of indices and palette.</returns>
        (IEnumerable<IndexData> indices, IList<Color> palette) Quantize(IEnumerable<Color> colors, QuantizationSettings settings);

        /// <summary>
        /// Encodes a collection of indices.
        /// </summary>
        /// <param name="indices">List of colors to encode.</param>
        /// <returns>Encoded index data.</returns>
        byte[] Save(IEnumerable<IndexData> indices);
    }
}
