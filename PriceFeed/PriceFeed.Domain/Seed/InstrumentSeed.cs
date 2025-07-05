using PriceFeed.Domain.Models;

namespace PriceFeed.Domain.Seed;

public static class InstrumentSeed
{
    public static List<Instrument> Instruments =>
    [
        new Instrument { Id = "BTCEUR" },
        new Instrument { Id = "BTCUSDT" },
        new Instrument { Id = "EURUSDT" }
    ];
}