﻿using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Xpo.DB.Exceptions;

namespace SchedulerBindXPOMvc {
    public abstract class BaseXpoController<T> : Controller where T : XPObject {
        UnitOfWork fSession;

        public BaseXpoController()
            : base() {
            fSession = CreateSession();
        }

        protected UnitOfWork XpoSession {
            get { return fSession; }
        }

        protected virtual UnitOfWork CreateSession() {
            return XpoHelper.GetNewUnitOfWork();
        }

        bool Save(BaseViewModel<T> viewModel, bool delete) {
            T model = XpoSession.GetObjectByKey<T>(viewModel.ID);
            if (model == null && !delete)
                model = (T)XpoSession.GetClassInfo<T>().CreateNewObject(XpoSession);
            if (!delete)
                viewModel.GetData(model);
            else if (model != null)
                XpoSession.Delete(model);
            try {
                model.Save();
                XpoSession.CommitChanges();
                return true;
            }
            catch (LockingException) { return false; }
        }

        protected bool Save(BaseViewModel<T> viewModel) {
            return Save(viewModel, false);
        }

        protected bool Delete(BaseViewModel<T> viewModel) {
            return Save(viewModel, true);
        }
    }
}