﻿using Microsoft.EntityFrameworkCore;
using QuakeLogger.Data.Context;
using QuakeLogger.Domain.Interfaces.Repositories;
using QuakeLogger.Domain.Models;
using QuakeLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuakeLogger.Data.Repositories
{
    public class QuakeGameRepo : IQuakeGameRepo
    {
        private readonly QuakeLoggerContext _context;

        public QuakeGameRepo(QuakeLoggerContext context)
        {
            _context = context;
        }

        public int Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();

            return game.Id;
        }        
        public Game FindById(int id)
        {
            return _context.Games
                .Where(i => i.Id == id)
                .Include(x => x.KillMethods)
                .Include(x=> x.GamePlayers)                
                .FirstOrDefault();
        }
        
        public List<Game> GetAll()
        {
            return _context.Games
                .Include(x => x.KillMethods)
                .ToList();
        }     

        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var game = _context.Games.First(i => i.Id == id);
            _context.Games.Remove(game);
            _context.SaveChanges();
        }
    }
}
