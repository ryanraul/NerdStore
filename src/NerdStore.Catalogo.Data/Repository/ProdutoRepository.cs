﻿using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Produto>> ObterTodos()
            => await _context.Produtos.AsNoTracking().ToListAsync();

        public async Task<Produto> ObterPorId(Guid id)
            => await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Categoria>> ObterCategorias()
            => await _context.Categorias.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
            => await _context.Produtos.AsNoTracking().Include(p => p.Categoria).Where(c => c.Categoria.Codigo == codigo).ToListAsync();

        public void Adicionar(Produto produto)
            => _context.Add(produto);

        public void Atualizar(Produto produto)
            => _context.Update(produto);

        public void Adicionar(Categoria categoria)
            => _context.Add(categoria);

        public void Atualizar(Categoria categoria)
            => _context.Update(categoria);

        public void Dispose()
            => _context?.Dispose();
    }
}
