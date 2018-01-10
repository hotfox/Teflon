using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teflon.SDK.Core
{
    public class TestEngine
    {
        public Context Context { get; private set; }

        public int ErrorCode { get; private set; }

        public TestEngine(Context context=null)
        {
            Context = context;
        }
        public virtual void Run(Product product)
        {
            DateTime start = DateTime.Now;
            ErrorCode = 0;
            try
            {
                ErrorCode = product.Run(Context);
            }
            catch (TeflonDoubleAssertFailException e)
            {
                SetErrorcode(e.ErrorCode);
            }
            catch (TeflonBoolAssertFailException e)
            {
                SetErrorcode(e.ErrorCode);
            }
            catch (TeflonIntAssertFailException e)
            {
                SetErrorcode(e.ErrorCode);
            }
            catch (TeflonStringAssertFailException e)
            {
                SetErrorcode(e.ErrorCode);
            }
            catch (TeflonCommunicationException e)
            {
                SetErrorcode(e.ErrorCode);
            }
            finally
            {
                DoubleVariable time = (DateTime.Now - start).TotalSeconds;
                time.Name = "TotalTestTime";
                SetErrorcode(ErrorCode);
                product.Dispose();
            }
        }
        private void SetErrorcode(int error_code)
        {
            this.ErrorCode = error_code;
            FailcodeVariable failcode = error_code;
            failcode.Name = "Failcode";
        }
    }
}
