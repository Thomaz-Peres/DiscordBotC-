﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg.RpgMonsters
{
    public class BaseMonstersEntity
    {
        public int MonsterId { get; set; }
        public string MonsterName { get; set; }
        public int MonsterLife { get; set; }
        public int MonsterMana { get; set; }
        public AtributesEntity MonsterAtributes { get; set; } = new AtributesEntity();
        public DateTime Spawn { get; set; }
    }
}
